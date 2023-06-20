#!/bin/bash
set -e

# Start RabbitMQ server in the background
rabbitmq-server -detached

sleep 20

# Check if the 'authentication' user exists
if ! rabbitmqctl list_users | grep -q '^authentication'; then
    # If not, add user and set permissions
    rabbitmqctl add_user authentication Password123!
    rabbitmqctl set_permissions -p / authentication ".*" ".*" ".*"
fi

# Check if the 'course_management' user exists
if ! rabbitmqctl list_users | grep -q '^course_management'; then
    # If not, add user and set permissions
    rabbitmqctl add_user course_management Password123!
    rabbitmqctl set_permissions -p / course_management ".*" ".*" ".*"
fi

# Stop the background RabbitMQ server
rabbitmqctl stop

# Start RabbitMQ server in the foreground
rabbitmq-server