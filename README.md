## Distributed Caching - Redis on .NET6 Demo ##

This project is created from [this blog post](https://nishanc.medium.com/redis-as-a-distributed-cache-on-net-6-0-949ef5b795ee)

### Docker ###

`docker run --name redis-local -p 5002:6379 -d redis`

⚠ Note: For the ease of accessing Redis from other containers via Docker networking, the “Protected mode” is turned off by default. This means that if you expose the port outside of your host (e.g., via `-p` on `docker run`), it will be open without a password to anyone. It is **highly** recommended to set a password (by supplying a config file) if you plan on exposing your Redis instance to the internet. [Read more](https://hub.docker.com/_/redis).

`docker exec -it redis-local sh`

`redis-cli`

`dbsize` - output should look like `(integer) 0`

After running demo:

From `redis-cli`: 

run `scan 0` (iterates the set of keys in the currently selected redis database)([see more](https://redis.io/commands/scan/))

`hgetall keyname` to see the list of fields and their values stored in the hash.  Use the `keyname` retrieved from running `scan 0`.


