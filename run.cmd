if "%1" == "--production" (
  docker-compose up --build
) else (
  docker-compose -f docker-compose.yml -f docker-compose.dev.yml up --build
)
