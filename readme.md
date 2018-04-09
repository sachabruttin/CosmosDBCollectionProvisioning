# Cosmos DB Collection Provisioning

Create a Cosmos DB Collection from command line.

## Parameters

| short | long | description | Default |
|-------|------|-------------|---------|
| `-u` | `--uri`  | Cosmos DB URI |
| `-k` | `--key`  | Key to access to DocumentDB |
| `-d` | `--databaseId`  | Cosmos DB database id (name) |
| `-c` | `--collection`  | Collection name to create |
| `-t` | `--throughput`  | Collection throughput | 400 |

The databaseId is automatically created if she doesn't already exists. 
Otherwise the collection is added to the existing database.