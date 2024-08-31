docker network create --driver=bridge --subnet=172.77.0.0/16 myLocalNetwork
SET COMPOSE_CONVERT_WINDOWS_PATHS=1

docker-compose -f docker.environment/workspace.docker-compose.yml up -d 
docker-compose -f docker.microservices/docker-compose.microservices.yml up --build

pause