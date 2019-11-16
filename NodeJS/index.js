"use strict";


const FS                = require("fs")
     ,PATH              = require("path")
     ,resolve           = function(path){ //normalize to Unix-slash (will work on Windows too).
                            path = path.replace(/\"/g,"");
                            path = path.replace(/\\+/g,"/");
                            path = PATH.resolve(path); 
                            path = path.replace(/\\+/g,"/"); 
                            path = path.replace(/\/\/+/g,"/"); 
                            return path;
                          }
     ,ARGS              = process.argv.filter(function(s){return false === /node\.exe/i.test(s) && false === /index\.js/i.test(s);})
     ,FILE_IN           = resolve(ARGS[0])
     ,FILE_IN_MIMETYPE  = require("./mimetypes.js").extension_to_mimetype( PATH.parse(FILE_IN).ext.toLowerCase().replace(/^\./g,"") )
     ,FILE_OUT          = FILE_IN + ".txt"
     ;


function write_handler(err){
  if(null !== err){
    console.error("[ERROR] problem writing to file:\r\n" + FILE_OUT + "\r\n", err);
    process.exitCode = 555; //error
  }else{
    process.exitCode = 0; //success
  }

  process.exit();
}


function read_handler(err, data){
  var output;

  if(null !== err){
    console.error("[ERROR] problem reading from file:\r\n" + FILE_IN + "\r\n", err);
    process.exitCode = 444;
    process.exit();
  }
  
  output =  "data:" + FILE_IN_MIMETYPE
            + (true === /text/i.test(FILE_IN_MIMETYPE) ? ";charset=UTF-8" : "")    //add an explicit UTF-8 parsing (ASCII will be fine, byte-style will be parsed to UTF-8, if you need Unicode change it to ';charset=UTF-16')
            + ";base64," + data.toString("base64")
            + "\r\n"
            ;

  FS.writeFile(FILE_OUT, output, {flag:"w", encoding:"utf8"}, write_handler) //overwrite
}


FS.readFile(FILE_IN, {flag:"r", encoding:null}, read_handler) //encoding is null for reading to buffer (explicit)

