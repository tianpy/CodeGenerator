# coding=utf-8

from generator.utility.dbhelper import dbhelper
from generator.utility.filehelper import filehelper
from config import config
from string import Template
import abc
import os
import uuid
import json
import platform


class AbstractGenerator(metaclass=abc.ABCMeta):
    _templatemapping = {}

    @abc.abstractproperty
    def _templaterootpath(self):
        raise NotImplementedError

    def __init__(self):
        self._templatemapping = {
            **config["db"], **config["project"], **config["domainmodel"]}

    def __call__(self):
        self._generatemapping()
        self.__generatefile()

    def __generatefile(self):
        templaterootpath = "./template/%s" % (self._templaterootpath)
        projectdistpath = "./dist/%s" % (self._templaterootpath)
        directories = os.walk(templaterootpath)

        # self._templatemapping["entityresultmaps"] = ''
        # self._templatemapping["allfieldsforinsert"] = ''
        # self._templatemapping["allfieldsforinsertparam"] = ''
        # self._templatemapping["allfieldsforselect"] = ''
        # self._templatemapping["allfieldsforjoin"] = ''
        # self._templatemapping["alltablesforjoin"] = ''
        # self._templatemapping["allfieldsforupdate"] = ''
        # self._templatemapping["allfieldsfororderby"] = ''
        # self._templatemapping["allfieldsforwhere"] = ''
        # self._templatemapping["getmodelbyid"] = ''
        # self._templatemapping["getentitybyid"] = ''
        # self._templatemapping["getpagedmodelsbyid"] = ''
        # self._templatemapping["getpagedentitiesbyid"] = ''
        # self._templatemapping["getcount"] = ''
        # self._templatemapping["insert"] = ''
        # self._templatemapping["update"] = ''

        projectrootpath = projectdistpath + "/" + self._doublesubstitute(
            "#{companycode}.#{appcode}.#{modulecode}.Service", self._templatemapping)
        filehelper.createdir(projectrootpath)
        self._projectrootpath_generated(projectrootpath)

        for row in directories:
            templateprojectpath, dirs, files = row[0], row[1], row[2]
            realprojectpath = projectrootpath + self._doublesubstitute(
                templateprojectpath.replace(templaterootpath, ''), self._templatemapping)
            for dirname in dirs:
                realdirname = self._doublesubstitute(
                    dirname, self._templatemapping)
                projectnextlayerpath = realprojectpath + "/" + realdirname
                filehelper.createdir(projectnextlayerpath)

                self._projectpath_generated(
                    projectrootpath, projectnextlayerpath, realdirname)

            for filename in files:
                templatefilename = filename
                realfilename = self._doublesubstitute(
                    filename, self._templatemapping)
                templatefilecontent = filehelper.readfile(
                    templateprojectpath, templatefilename)
                realfilecontent = self._doublesubstitute(
                    templatefilecontent, self._templatemapping)
                filehelper.createfile(
                    realprojectpath, realfilename, realfilecontent)

                self._projectfile_generated(
                    projectrootpath, realprojectpath, realfilename)

        print("{0}generate success{0}".format("="*10))
        self.__opengeneratefolder(projectrootpath)

    @abc.abstractmethod
    def _generatemapping(self):
        raise NotImplementedError

    @abc.abstractmethod
    def _projectrootpath_generated(self, rootpath):
        raise NotImplementedError

    @abc.abstractmethod
    def _projectpath_generated(self, rootpath, currentpath, currentpathname):
        raise NotImplementedError

    @abc.abstractmethod
    def _projectfile_generated(self, rootpath, currentpath, filename):
        raise NotImplementedError

    def _doublesubstitute(self, text, dicobj):
        first = TeldTemplate(text)(dicobj)
        second = TeldTemplate(first)(dicobj)
        return second

    def __opengeneratefolder(self,projectrootpath):
        system = platform.system()
        path = os.path.abspath(projectrootpath)
        if system == 'Windows':
            os.startfile(path)
        elif system == 'Linux':
            os.system('cd %s;' % path)
        pass
    
class TeldTemplate(Template):
    delimiter = "#"
    idpattern = r'[.a-z][_a-z][_a-z0-9][.a-z0-9]*'

    def __call__(self, dictobject):
        try:
            return self.safe_substitute(dictobject)
        except:
            pass
