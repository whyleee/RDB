@echo off
if "%1" == "--production" (
  docker-compose -f docker-compose.yml -f docker-compose.production.yml up --build
) else if "%1" == "--azure" (
  docker-compose -f docker-compose.yml -f docker-compose.azure.yml up -d --build
) else (
  docker-compose up --build
)
