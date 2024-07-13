# Description
Send messages via `RabbitMQ`

Project has 2 console apps: 
- `Publisher` - sends messages
- `Consumer` - receives messages

## Launch
- Need to launch `RabbitMQ` (for example in `docker`)
- Need to configure `app.config` in the root (set `HostName` as `RabbitMQ` address)
- Run apps

## Addition
Both apps use common `app.config`
