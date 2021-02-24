@echo off

echo "Gerando imagem"

docker build --pull --rm -f "Dockerfile" -t gazinportalapi:latest "."

echo "Exportando imagem"

docker save -o gazinportalapi-latest.tar gazinportalapi:latest

pause