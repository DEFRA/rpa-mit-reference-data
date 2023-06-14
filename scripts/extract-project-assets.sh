echo "copying project.assets.json from container"
cp -r ./obj/RPA.MIT.ReferenceData.Api/project.assets.json ./RPA.MIT.ReferenceData.Api/obj/project.assets.json
echo "making project.assets.json writable by all so Jenkins can delete it"
chmod 666 ./RPA.MIT.ReferenceData.Api/obj/project.assets.json