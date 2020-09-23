# coding=utf-8
import os

class filehelper:
    @staticmethod
    def createfile(savepath,filename,filecontent):
        isexists=os.path.exists(savepath) 
        if not isexists:
            os.makedirs(savepath) 
        with open(savepath +"/"+ filename,'w+', encoding='UTF-8') as f:
            f.write(filecontent)

    @staticmethod
    def createdir(path):
        isexists=os.path.exists(path)
        if not isexists:
            os.makedirs(path) 

    @staticmethod
    def readfile(filepath,filename):
        path = filepath +'/' +filename
        isexists=os.path.exists(path)
        if isexists:
            with open(path,'r', encoding='UTF-8') as f:
                return f.read()
        else:
            return ''

    @staticmethod
    def wirtefile(filepath,filename,filecontent):
        path = filepath + '/' + filename
        isexists=os.path.exists(path)
        if isexists:
            with open(path,'w', encoding='UTF-8') as f:
                return f.write(filecontent)
        else:
            filehelper.createfile(filepath,filename,filecontent)
