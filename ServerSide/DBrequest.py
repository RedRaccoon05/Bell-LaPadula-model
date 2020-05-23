import sqlite3
import Data_Model
import re
def dbAuth(Data_Authentication):
    conn = sqlite3.connect("DataBase.db")
    cursor = conn.cursor()
    sql = 'SELECT COUNT(login) FROM Users WHERE login=\'{}\' AND password=\'{}\''.format(Data_Authentication.login,Data_Authentication.password)
    result = cursor.execute(sql)
    result = cursor.fetchone()
    result = re.search('(\d+)',str(result))
    result = result.group(0)
    if result == '1':
        sql = 'SELECT access FROM Users WHERE login = \'{}\''.format(Data_Authentication.login)
        result = cursor.execute(sql)
        result = cursor.fetchone()
        result = re.search('(\d+)',str(result))
        result = result.group(0)
        return result
    return "Не правильный логин или пароль"

def dbReg(Data_Authentication):
    conn = sqlite3.connect("DataBase.db")
    cursor = conn.cursor()
    sql = 'SELECT EXISTS(SELECT login FROM Users WHERE login = \'{}\')'.format(Data_Authentication.login)
    cursor.execute(sql)
    result = cursor.fetchone()
    result = re.search('(\d+)',str(result))
    result = result.group(0)
    if result == '1':
        return "Пользователь уже существует"
    sql = 'INSERT INTO Users VALUES(\'{}\',\'{}\',\'{}\')'.format(Data_Authentication.login,Data_Authentication.password,3)
    cursor.execute(sql)
    conn.commit()
    return "Ok"

def dbChangePassword(Data_Change):
    conn = sqlite3.connect("DataBase.db")
    cursor = conn.cursor()
    sql = 'UPDATE Users SET password = \'{}\' WHERE login = \'{}\''.format(Data_Change.password, Data_Change.login)
    cursor.execute(sql)
    conn.commit()
    return "Ok"

def dbNoteRes(SecFlag):
    conn = sqlite3.connect("DataBase.db")
    cursor = conn.cursor()
    sql = "SELECT Name, Data, SecFlag FROM Notes WHERE SecFlag >=\'{}\'".format(SecFlag)
    cursor.execute(sql)
    return cursor.fetchall()

def dbNoteAdd(Note):
    conn = sqlite3.connect("DataBase.db")
    cursor = conn.cursor()
    sql = "INSERT INTO Notes VALUES(NULL,\'{}\',\'{}\',\'{}\')".format(Note.name,Note.secflag,Note.data)
    cursor.execute(sql)
    conn.commit()
    return "Ok"


def dbNoteDelete(Note):
    conn = sqlite3.connect("DataBase.db")
    cursor = conn.cursor()
    sql = "DELETE FROM Notes WHERE Name = \'{}\' ".format(Note.name)
    cursor.execute(sql)
    conn.commit()
    return "Ok"

def dbNoteExist(Note):
    conn = sqlite3.connect("DataBase.db")
    cursor = conn.cursor()
    sql = 'SELECT EXISTS(SELECT Name FROM Notes WHERE Name = \'{}\')'.format(Note.name)
    cursor.execute(sql)
    result = cursor.fetchone()
    result = re.search('(\d+)',str(result))
    result = result.group(0)
    if result != '0':
        return "No"
    return "Ok"