#!/bin/bash

echo 'Bagend Docker Image Updater'
echo 'v0.0.1'
echo '--------'

dumpDockerImages() {
    echo 'deleting cached docker images'
    docker rmi -f $(docker images -aq)
}

if [ "$1" == '-d' ]; then
    dumpDockerImages
    fi

echo 'building bagend-event-api'
git stash
git pull
git stash pop

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

if [ "$1" == '-r' ]; then
    echo 'restarting bagend.service'
    systemctl restart bagend
    fi