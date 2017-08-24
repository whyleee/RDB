if "%1" == "--production" (
  docker-compose up -d --no-deps --build rdb.api
) else (
  docker-compose -f ..\docker-compose.yml -f ..\docker-compose.dev.yml up -d --no-deps --build rdb.api
)
