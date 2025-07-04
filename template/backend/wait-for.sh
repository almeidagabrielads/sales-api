cat > wait-for.sh << 'EOF'
#!/usr/bin/env bash
# usage: wait-for.sh host:port -- command args
host_port="$1"; shift
cmd="$@"
IFS=':' read -r host port <<< "$host_port"
until nc -z "$host" "$port"; do
  >&2 echo "Aguardando $host_port..."
  sleep 1
done
>&2 echo "$host_port disponível — iniciando testes"
exec $cmd
EOF