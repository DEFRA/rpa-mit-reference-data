psql -U "$POSTGRES_USERNAME" -Atc "select tablename from pg_tables where schemaname='public'" "$POSTGRES_DB" | while read -r TBL ; do
    pg_dump -U postgres --column-inserts --data-only --table="$TBL" "$POSTGRES_DB" > /home/postgres/seed-data-scripts/"$TBL".sql
done