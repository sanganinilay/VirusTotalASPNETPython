import sys
import postfile
host = "www.virustotal.com"
selector = "https://www.virustotal.com/vtapi/v2/file/scan"
fields = [("apikey", "Enter Your API Key")]
file_to_send = open(sys.argv[1], "rb").read()
files = [("file",sys.argv[1],file_to_send)]
json = postfile.post_multipart(host, selector, fields, files)
print json
