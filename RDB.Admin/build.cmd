@echo off
if "%1" == "--production" (
  docker-compose -f ..\docker-compose.yml -f ..\docker-compose.production.yml up -d --no-deps --build admin
) else if "%1" == "--azure" (
  docker-compose -f ..\docker-compose.yml -f ..\docker-compose.azure.yml up -d --no-deps --build admin
) else if "%1" == "--debug" (
  set DOCKERFILE_EXT=.debug
  docker-compose up -d --no-deps --build admin
) else (
  docker-compose up -d --no-deps --build admin
)
