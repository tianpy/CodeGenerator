# coding=utf-8

from config import config
import pymssql

class dbhelper:
    def __init__(self):
        dbconfig = config["db"]
        self.server,self.user,self.password,self.database = dbconfig["host"]+':'+dbconfig["port"],dbconfig["username"],dbconfig["password"],dbconfig["database"]
        self.nullabledatatype = ["tinyint","smallint","int","bigint","date","time","datetime","datetime2","smalldatetime","float","decimal"]
        self.csharpdatatypemapping = {'tinyint':'int','smallint':'int','int':'int','bigint':'int',
                                      'date':'DateTime','time':'DateTime','datetime':'DateTime','datetime2':'DateTime','smalldatetime':'DateTime',
                                      'float':'float','decimal':'decimal',
                                      'varchar':'string','char':'string','nvarchar':'string'
                                     }

    def gettablefields(self,tablename):
        rows = self.__executequery("""select
                                        a.name as FieldName,
                                        b.name as FieldType
                                        from sys.syscolumns a with (nolock)
                                        left join sys.systypes b on a.xusertype=b.xusertype
                                        inner join sys.sysobjects d on a.id=d.id and d.name<>'dtproperties'
                                        where d.name ='%s'""",tablename)
        
        primarykey = self.getprimarykey(tablename)

        tablefields = []
        for row in rows:
            name = row[0]
            datatype = row[1]
            isprimarykey = name == primarykey
            nullable = datatype in self.nullabledatatype
            datatype = self.csharpdatatypemapping[datatype]
            tablefields.append(fields(name,isprimarykey,nullable,datatype))
        
        return tablefields

    def getprimarykey(self,tablename):
        rows = self.__executequery("""SELECT SYSCOLUMNS.name
                                        FROM SYSCOLUMNS,SYSOBJECTS,SYSINDEXES,SYSINDEXKEYS 
                                        WHERE SYSCOLUMNS.id = object_id('%s')
                                        AND SYSOBJECTS.xtype = 'PK'
                                        AND SYSOBJECTS.parent_obj = SYSCOLUMNS.id 
                                        AND SYSINDEXES.id = SYSCOLUMNS.id 
                                        AND SYSOBJECTS.name = SYSINDEXES.name 
                                        AND SYSINDEXKEYS.id = SYSCOLUMNS.id 
                                        AND SYSINDEXKEYS.indid = SYSINDEXES.indid
                                        AND SYSCOLUMNS.colid = SYSINDEXKEYS.colid""",tablename)
        return rows[0][0] if rows.__len__() > 0 else '' 

    def __executequery(self,selectsql,*args):
        sql = selectsql %(args)
        rows = []
        with pymssql.connect(server=self.server,user=self.user,password=self.password,database=self.database) as conn:
            cursor = conn.cursor()
            cursor.execute(sql)
            [rows.append(row) for row in cursor]
            return rows

class fields:
    name,isprimarykey,nullable,datatype='',False,False,''
    def __init__(self,name,isprimarykey,nullable,datatype):
        self.name=name
        self.isprimarykey=isprimarykey
        self.nullable=nullable
        self.datatype=datatype

    def __str__(self):
        return "Name:%s,IsPrimaryKey:%s,Nullable:%s,DataType:%s" %(self.name,self.isprimarykey,self.nullable,self.datatype)