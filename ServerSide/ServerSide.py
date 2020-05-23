
import select
import socket
import Data_Model
import json
import DBrequest
import re
import base64
SERVER_ADDRESS = ('127.0.0.1', 1337)

# Говорит о том, сколько дескрипторов единовременно могут быть открыты
MAX_CONNECTIONS = 10

# Откуда и куда записывать информацию
INPUTS = list()
OUTPUTS = list()

def DataParser(data):
    deser = json.loads(data)
    # поле operation определяет операцию для действий с логином и/или паролем, т. е. там где указан type равный auth
    if  str(type(deser))=='<class \'dict\'>' and deser['type'] == 'auth':
        if deser['operation'] == 'authentication':
            Data_Authentication = Data_Model.Authentication(deser['login'],deser['password'],deser['operation'])
            result = DBrequest.dbAuth(Data_Authentication)
            return result
            
        elif deser['operation'] == 'registration':
            Data_Registration = Data_Model.Authentication(deser['login'],deser['password'],deser['operation'])
            return DBrequest.dbReg(Data_Registration)

        elif deser['operation'] == 'changepassword':#смена пароля
            Data_Change_Password = Data_Model.Authentication(deser['login'],deser['password'],deser['operation'])
            return DBrequest.dbChangePassword(Data_Change_Password)
        #type для заметок: добавление, удаление, проверка на существование
    elif deser['type'] == 'addNote':
        Data_Notes = Data_Model.Note(deser['name'],deser['data'],deser['secflag'])
        return DBrequest.dbNoteAdd(Data_Notes)
    elif deser['type'] == 'deleteNote':
        Data_Notes = Data_Model.Note(deser['name'],deser['data'],deser['secflag'])
        return DBrequest.dbNoteDelete(Data_Notes)
    elif deser['type'] == 'checkexist':#Проверка на существование заметки с таким же названием
        Data_Notes = Data_Model.Note(deser['name'],deser['data'],deser['secflag'])
        return DBrequest.dbNoteExist(Data_Notes)

def Notes_Response(flag):
    res = DBrequest.dbNoteRes(flag)
    notes = []
    for note_list in res:
        notes.append(Data_Model.Note(note_list[0],note_list[1],note_list[2]))
    ob = Data_Model.Notes(notes)
    a = json.dumps(ob,indent = 3,cls = Data_Model.NotesEncoder,ensure_ascii=False)
    return a

def Notes_Access_Parser(data):#Парс уровня доступа пользователя
    try:
        result = re.search(str('GetNotes'),str(data))
        result = result.group(0)
        if result == "GetNotes":
            result = re.search('(\d+)',str(data))
            result = result.group(0)
            return True, result
        return False,0
    except : 
        return False,0

def get_non_blocking_server_socket():

    # Создаем сокет, который работает без блокирования основного потока
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.setblocking(0)

    # Биндим сервер на нужный адрес и порт
    server.bind(SERVER_ADDRESS)

    # Установка максимального количество подключений
    server.listen(MAX_CONNECTIONS)

    return server


def handle_readables(readables, server):
    """
    Обработка появления событий на входах
    """
    for resource in readables:

        # Если событие исходит от серверного сокета, то мы получаем новое подключение
        if resource is server:
            connection, client_address = resource.accept()
            connection.setblocking(0)
            INPUTS.append(connection)
            print("new connection from {address}".format(address=client_address))

        # Если событие исходит не от серверного сокета, но сработало прерывание на наполнение входного буффера
        else:
            data = ""
            try:
                data = resource.recv(10000)

            # Если сокет был закрыт на другой стороне
            except ConnectionResetError:
                print("Error! Connect close.")

            if data:
                data = base64.b64decode(data)
                

                print("getting data: {data}".format(data=data.decode('utf-8')))
                accessFlag_Check, Access_Flag = Notes_Access_Parser(data)
                if accessFlag_Check:
                    result = Notes_Response(Access_Flag)
                    
                else: 
                    result = DataParser(data)
                send_res = result.encode("UTF-8")
                result = base64.b64encode(send_res)
                result = result.decode('utf-8')
                resource.send(bytes(str(result), encoding='UTF-8'))
                # Говорим о том, что мы будем еще и писать в данный сокет
                if resource not in OUTPUTS:
                    OUTPUTS.append(resource)

            # Если данных нет, но событие сработало, то ОС нам отправляет флаг о полном прочтении ресурса и его закрытии
            else:

                # Очищаем данные о ресурсе и закрываем дескриптор
                clear_resource(resource)


def clear_resource(resource):
    """
    Метод очистки ресурсов использования сокета
    """
    if resource in OUTPUTS:
        OUTPUTS.remove(resource)
    if resource in INPUTS:
        INPUTS.remove(resource)
    resource.close()

    print('closing connection ' + str(resource))


def handle_writables(writables):

    # Данное событие возникает когда в буффере на запись освобождается место
    for resource in writables:
        try:
       #     resource.send(bytes('Hello from server!', encoding='UTF-8'))
             pass
        except OSError:
            clear_resource(resource)


if __name__ == '__main__':

    # Создаем серверный сокет без блокирования основного потока в ожидании подключения
    server_socket = get_non_blocking_server_socket()
    INPUTS.append(server_socket)

    print("server is running, please, press ctrl+c to stop")
    try:
        while INPUTS:
            readables, writables, exceptional = select.select(INPUTS, OUTPUTS, INPUTS)
            handle_readables(readables, server_socket)
            handle_writables(writables)
    except KeyboardInterrupt:
        clear_resource(server_socket)
        print("Server stopped! Thank you for using!")