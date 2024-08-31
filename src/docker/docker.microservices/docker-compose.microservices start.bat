docker network create --driver=bridge --subnet=172.75.0.0/16 myLocalNetwork
SET COMPOSE_CONVERT_WINDOWS_PATHS=1
docker-compose -f docker-compose.microservices.yml up --build

pause