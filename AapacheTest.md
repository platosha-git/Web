# ApacheBenchmark

**Прямой доступ с одним инстансом** 
```
platosha@platosha-UX310UAK:~$ ab -c 10 -n 1000 http://localhost:5001/api/v1/tours
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


Server Software:        
Server Hostname:        localhost
Server Port:            5001

Document Path:          /api/v1/tours
Document Length:        0 bytes

Concurrency Level:      10
Time taken for tests:   0.556 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      0 bytes
HTML transferred:       0 bytes
Requests per second:    1799.68 [#/sec] (mean)
Time per request:       5.557 [ms] (mean)
Time per request:       0.556 [ms] (mean, across all concurrent requests)
Transfer rate:          0.00 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.0      0       1
Processing:     0    0   0.6      0      18
Waiting:        0    0   0.0      0       0
Total:          0    1   0.6      0      18
WARNING: The median and mean for the total time are not within a normal deviation
        These results are probably not that reliable.

Percentage of the requests served within a certain time (ms)
  50%      0
  66%      0
  75%      0
  80%      1
  90%      1
  95%      1
  98%      1
  99%      1
 100%     18 (longest request)
```

**Балансировка с тремя инстансами**
```
platosha@platosha-UX310UAK:~$ ab -c 10 -n 1000 http://localhost/api/v1/tours
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
Server Port:            80

Document Path:          /api/v1/tours
Document Length:        178 bytes

Concurrency Level:      10
Time taken for tests:   0.086 seconds
Complete requests:      1000
Failed requests:        0
Non-2xx responses:      1000
Total transferred:      371000 bytes
HTML transferred:       178000 bytes
Requests per second:    11604.56 [#/sec] (mean)
Time per request:       0.862 [ms] (mean)
Time per request:       0.086 [ms] (mean, across all concurrent requests)
Transfer rate:          4204.39 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.1      0       1
Processing:     0    0   0.1      0       1
Waiting:        0    0   0.1      0       1
Total:          1    1   0.1      1       1

Percentage of the requests served within a certain time (ms)
  50%      1
  66%      1
  75%      1
  80%      1
  90%      1
  95%      1
  98%      1
  99%      1
 100%      1 (longest request)
```
**Тестирование балансировки**
5 раз выполняется GET-запрос 
*Лог-файл 1*
![](https://github.com/platosha-git/Web/blob/main/source/nginx/log1.png)
*Лог-файл 2*
![](https://github.com/platosha-git/Web/blob/main/source/nginx/log2.png)
*Лог-файл 3*
![](https://github.com/platosha-git/Web/blob/main/source/nginx/log3.png)
