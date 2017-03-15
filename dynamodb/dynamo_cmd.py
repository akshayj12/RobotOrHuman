from boto.dynamodb2.layer1 import DynamoDBConnection
import sys, getopt
import boto.dynamodb2
from boto.dynamodb2.fields import HashKey, RangeKey, KeysOnlyIndex, GlobalAllIndex
from boto.dynamodb2.table import Table
from collections import Counter
import json
import datetime
from datetime import date, timedelta,datetime
def tableExistence(inputName,region):

  connection=boto.dynamodb2.connect_to_region(region)  
	tables = connection.list_tables()
        for i in tables["TableNames"]:
            if(inputName == i):
                return 1
        return 0

def createTable(tableName, fileName,region):
    done = True
    with open(fileName) as data_file:
        data = json.load(data_file)

    hash_key = data["hash_key"]
    
    #print data["columns"]
    try:
        users = Table.create(tableName, schema=[
            HashKey(hash_key), # defaults to STRING data_type
            ], throughput={
              'read': 5,
               'write': 5,
           },
          connection=boto.dynamodb2.connect_to_region(region)
           )
    except:
         done = False
    return done

def  insertData(tableName, region, data):
    done = True
    if(tableExistence(tableName,region) != 0):
        users = Table(tableName)
        users.put_item(data, overwrite=True)
    else:
        print "table does not exist"
        done = False
    return done

#   How to use
#   tt = getItem('ListProcessing','us-east-1','33')
#   print tt['Name']

def getItem(tableName, region,keyVal):
    if(tableExistence(tableName,region) != 0):

        list_item_table = Table(tableName)
        try:
          item = list_item_table.get_item(list_id=keyVal)
          return item
        except boto.dynamodb2.exceptions.ItemNotFound:
          return None
    else:
        print "Table does  not exist -- ", tableName
 
#updateStatus('ListProcessing', 'us-east-1', '14', 'deleted')
def updateStatus(tableName, region,idVal,statusVal):
    done = False
    item = getItem(tableName, region,idVal)
    if(item!=None):
        item['Status'] = statusVal
        item.save(overwrite=True)
        done = True
    return done

    
def keyExists(tableName, region, id):
   x = True
   if(tableExistence(tableName,region) != 0):

        list_item_table = Table(tableName)
        try:
          item = list_item_table.get_item(seg_id =id)
        except boto.dynamodb2.exceptions.ItemNotFound:
          x = False
   return x