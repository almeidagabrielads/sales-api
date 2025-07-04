#!/bin/bash

set -e

host_port="$1"
shift
host="${host_port%:*}"
port="${host_port#*:}"

echo "Waiting for $host:$port..."

until nc -z "$host" "$port"; do
  >&2 echo "Service not yet available on $host:$port. Retrying..."
  sleep 1
done

echo "$host:$port is available — running tests..."
exec "$@"
