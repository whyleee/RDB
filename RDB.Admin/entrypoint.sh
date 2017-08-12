#!/bin/bash

# switch to local user for linux hosts to fix volume files access rights on the host
if [[ $(uname -s) != *moby ]]; then
  USER_ID=${LOCAL_USER_ID:-1000}

  if [[ $(id -u user) != $USER_ID ]]; then
    echo "Creating user UID=$USER_ID and setting access rights"

    useradd -g root -u $USER_ID -o -c "" -d /root -s /bin/bash user
    export HOME=/root

    chown user /root/
    chown user /root/.nuget
    chown user /root/.nuget/packages
    chown user /root/.nuget/packages/.tools
    chown user /root/.npm
    chown user /root/.npm/*
    chown -R user /root/.aspnet
    chown -R user /root/.local
    chown -R user /tmp/NuGetScratch
    chown -R user /app/bin
    chown -R user /app/obj
  fi

  echo "Starting with UID=$USER_ID"
  exec /usr/local/bin/gosu user "$@"
else
  exec "$@"
fi
