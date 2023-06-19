#!/bin/bash

rabbitmq-server
sleep 20

rabbitmqctl add_user authentication Password123!
rabbitmqctl set_permissions -p / authentication ".*" ".*" ".*"

rabbitmqctl add_user course_management Password123!
rabbitmqctl set_permissions -p / course_management ".*" ".*" ".*"