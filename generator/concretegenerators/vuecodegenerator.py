# coding=utf-8

from generator.abstractgenerator import AbstractGenerator

class VueCodeGenerator(AbstractGenerator):
    
    def _templaterootpath(self):
        return 'vue'
