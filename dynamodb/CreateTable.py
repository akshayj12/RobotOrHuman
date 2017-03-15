import sys
from dynamo_cmd import tableExistence

def main():
 
    length = len(sys.argv)
    if (length < 4):
       print " Only " + str(length-1) + "  arguments passed at command line 3 expected"
       print "Please input 3 command line arguments : "
       print "<coast(east|ireland|EU>"
       print "<table name> "
       print "<schemaName>"
       return

    coast = sys.argv[1]
   
    if (coast == 'east'):
       region = 'us-east-1' 
    elif((coast == 'EU') | (coast == 'ireland')):
       region = 'eu-west-1'
    else:
       print "Invalid region as first argument"
       return
 
    tableName = sys.argv[2]
    if(tableExistence(tableName,region) == 0):
        schemaName = sys.argv[3]
        createTable(tableName,schemaName,region)
        time.sleep(10)

main()