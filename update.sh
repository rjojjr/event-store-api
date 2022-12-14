#!/bin/bash

git stash
git pull
git stash pop

echo 'building bagend-event-api'
docker build -t bagend-event-api .

cd ../bagend-web-scraper
echo 'building bagend-web-scraper'
git pull
docker build -t bagend-web-scraper .

cd ../bagend-ml
echo 'building bagend-ml'
git pull
docker build -t bagend-ml .

cd ../event-store-api

if [[ "$1" == '-r' ]]; then
  echo 'restarting bagend.service'
  systemctl restart bagend
  fi

