# GGShop-marketProject-
This project was closed too far and this code in master branch

The options for deploying docker in the release branch, because I needed to rebuild most of the project to do this since the project is too old

(Основной код в ветке мастер, перестроенный код с развертыванием в докере в ветке релиз)


Befor Publication change secure keys from IdentityServer 
 
Build instruction(also do this for correct developing because you need to build all to dependency resolution): 

At first you should to build all release versions(this need for dependency resolution)

  1. Build ShopDb release 
  2. Build IdentityServer release 
  3. Build ShopApi and FileServer (the sequence is not important)
    
Then you can build publish versions and other

Also do this build scheme if you updated 1 or more components(you just can update only needed dll files)



