import json
from json import JSONEncoder
class Authentication:
    def __init__(self, login, password, operation):
        self.login = login
        self.password = password
        self.operation = operation

class AuthenticationEncoder(JSONEncoder):
        def default(self, o):
            return o.__dict__
class Note:
    def __init__(self, name, data, secflag):
        self.name = name
        self.data = data
        self.secflag = secflag
class Notes:
    def __init__(self, notes_):
        self.notes_ = list(notes_)
class NotesEncoder(JSONEncoder):
    def default(self,o):
        return o.__dict__