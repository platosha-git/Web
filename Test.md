# Web
**Цель работы:** сервис подбора тура по заданным параметрам.

**Предоставляемая возможность:** бронирование тура по указанным параметрам (направление, даты, категория отеля и т.п.)

**Функциональные требования:** 
- Гость иеет возможность подбора тура по параметрам.
- Пользователь имеет возможноть подбора, бронирования, просмотра и отмены брони.
- Менеджер иммет возможность просмотра, добавления и удаления туров, отелей, питания. А также просмотра пользователей.

platosha@platosha-UX310UAK:~$ ab -n 1000 -c 10 https://localhost/api/v1/Tours/1
This is ApacheBench, Version 2.3 <$Revision: 1843412 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 100 requests
Completed 200 requests
Completed 300 requests
Completed 400 requests
Completed 500 requests
Completed 600 requests
Completed 700 requests
Completed 800 requests
Completed 900 requests
Completed 1000 requests
Finished 1000 requests


Server Software:        Tours
Server Hostname:        localhost
Server Port:            443
SSL/TLS Protocol:       TLSv1.2,ECDHE-RSA-AES256-GCM-SHA384,2048,256
Server Temp Key:        X25519 253 bits
TLS Server Name:        localhost

Document Path:          /api/v1/Tours/1
Document Length:        123 bytes

Concurrency Level:      10
Time taken for tests:   3.156 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      303000 bytes
HTML transferred:       123000 bytes
Requests per second:    316.88 [#/sec] (mean)
Time per request:       31.558 [ms] (mean)
Time per request:       3.156 [ms] (mean, across all concurrent requests)
Transfer rate:          93.76 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        2    5   2.1      4      15
Processing:     8   26  13.1     24     148
Waiting:        8   26  13.0     24     147
Total:         13   31  13.3     29     156

Percentage of the requests served within a certain time (ms)
  50%     29
  66%     32
  75%     34
  80%     35
  90%     40
  95%     47
  98%     69
  99%    111
 100%    156 (longest request)


