docker network create --driver=bridge --subnet=172.75.0.0/16 myLocalNetwork
SET COMPOSE_CONVERT_WINDOWS_PATHS=1
docker-compose -f workspace.docker-compose.yml up

pause