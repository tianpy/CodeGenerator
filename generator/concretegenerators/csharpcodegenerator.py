# coding=utf-8

from generator.abstractgenerator import AbstractGenerator
from generator.utility.filehelper import filehelper
import json
import uuid
from generator.utility.dbhelper import dbhelper,fields

class CsharpCodeGenerator(AbstractGenerator):
    @property
    def _templaterootpath(self):
        return 'csharp'

    _tablefields = []

    def _generatemapping(self):
        self._tablefields = dbhelper().gettablefields(self._templatemapping['tablename'])
        self.__generateentitymapping()
        self.__generatemodelmapping()
        self.__generatesqlmapping()

    def _projectrootpath_generated(self,rootpath):
        self.__generatesolutionid(rootpath)

    def _projectpath_generated(self,rootpath,currentpath,currentpathname):
        self.__generateprojectid(rootpath,currentpathname)

    def _projectfile_generated(self,rootpath,currentpath,filename):
        pass
    
    def __generatesolutionid(self,savepath):
        key = self._doublesubstitute("#{companycode}.#{appcode}.#{modulecode}.Service",self._templatemapping)
        filename = 'SolutionInfo.config'
        filecontent = filehelper.readfile(savepath,filename)
        solutionid = ''
        if filecontent != '':
            jsonobj = json.loads(filecontent)
            if jsonobj.__contains__(key):
                solutionid = jsonobj[key]
       
        if solutionid == '':
            solutionid = str(uuid.uuid1())
            jsonobj = json.loads('{}') if filecontent == '' else json.loads(filecontent)
            jsonobj[key] = solutionid
            filehelper.createfile(savepath,filename,json.dumps(jsonobj))

        self._templatemapping[key+".SolutionID"] = solutionid
    
    def __generateprojectid(self,savepath,key):
        filename = 'ProjectInfo.config'
        filecontent = filehelper.readfile(savepath,filename)
        projectid = ''
        if filecontent != '':
            jsonobj = json.loads(filecontent)
            if jsonobj.__contains__(key):
                projectid = jsonobj[key]
       
        if projectid == '':
            projectid = str(uuid.uuid1())
            jsonobj = json.loads('{}') if filecontent == '' else json.loads(filecontent)
            jsonobj[key] = projectid
            filehelper.createfile(savepath,filename,json.dumps(jsonobj))

        self._templatemapping[key+".ProjectID"] = projectid

    def __generateentitymapping(self):
        entitymappingstr = self.__generatefieldsstr()
        self._templatemapping['entityfields'] = entitymappingstr

    def __generatemodelmapping(self):
        modelmappingstr = self.__generatefieldsstr()

        for key in self._templatemapping['modelpropertyrelations']:
            modelmappingstr += '''
        /// <summary>
        /// 
        /// </summary>
        public string %s { get; set; }''' %(key)
        
        self._templatemapping['modelfields'] = modelmappingstr

    def __generatefieldsstr(self):
        fieldstr = ''
        for field in self._tablefields:
            fieldstr += '''
        /// <summary>
        /// 
        /// </summary>
        public %s%s %s { get; set; }''' %(field.datatype,'?' if field.nullable else '',field.name)
        return fieldstr

    def __generatesqlmapping(self):        
        fieldnames = [field.name for field in self._tablefields]
        modelproperties = [key for key in self._templatemapping['modelpropertyrelations']]
        modelpropertyfields = ['%s as %s'%(v,k) for k,v in self._templatemapping['modelpropertyrelations'].items()]
        leftjointalbes = ['left join %s on M.%s = %s' %(v.split('.')[0],k,v) for k,v in self._templatemapping['foreignkeyrelations'].items()]
        orderbyfields = [orderby if '.' in orderby else 'M.'+ orderby for orderby in self._templatemapping['orderby']] 

        allfieldsforinsert = ','.join(fieldnames)
        self._templatemapping['allfieldsforinsert'] = allfieldsforinsert

        allfieldsforinsertparam ='#'+'#,#'.join(fieldnames)+'#'
        self._templatemapping['allfieldsforinsertparam'] = allfieldsforinsertparam

        allfieldsforselect = ','.join(fieldnames + modelproperties)
        self._templatemapping['allfieldsforselect'] = allfieldsforselect
        
        if len(modelpropertyfields)>0:
            allfieldsforjoin = 'M.' + ',M.'.join(fieldnames) + ',' + ','.join(modelpropertyfields)
            self._templatemapping['allfieldsforjoin'] = allfieldsforjoin
        else:self._templatemapping['allfieldsforjoin'] = ''

        if len(leftjointalbes)>0:
            alltablesforjoin ="\r\n".join(leftjointalbes) 
            self._templatemapping['alltablesforjoin'] = alltablesforjoin
        else:self._templatemapping['alltablesforjoin'] = ''

        if len(orderbyfields)>0:
            allfieldsfororderby ="\r\n".join(orderbyfields) 
            self._templatemapping['allfieldsfororderby'] = allfieldsfororderby
        else:self._templatemapping['allfieldsfororderby'] = ''

        allfieldsforupdate = ''
        for field in self._tablefields:
            if not field.isprimarykey:
                allfieldsforupdate += '''
        <isNotNull prepend="," property="{0}">
          {0} = #{0}#
        </isNotNull>'''.format(field.name)
        
        self._templatemapping['allfieldsforupdate'] = allfieldsforupdate

        allfieldsforwhere = ''
        for field in self._tablefields:
            allfieldsforwhere += '''
        <isNotEmpty prepend="and" property="{0}">
          M.{0} = #{0}#
        </isNotEmpty>'''.format(field.name)
        
        self._templatemapping['allfieldsforwhere'] = allfieldsforwhere

        pass
        
