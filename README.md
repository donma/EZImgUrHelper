# EZImgUrHelper


A simple library for .Net developer to upload image to ImgUr and some operations about album.
Happy Coding :)

Tutorial
----

一個簡單的 Library ，讓你可以上傳圖片到 ImgUr 
1. 你得先去[這邊](https://api.imgur.com/oauth2/addclient)申請 開發者帳號 你要拿到  client id 之後就可以了

2. 這邊都是採取匿名處理，所以要自己好好管理自己的deleteHash

3. 我這邊沒有處理將圖片從相簿中移除關聯，因為那 API 我怎麼測試都不成功，所以我索性就是寫直接把圖片刪除

4. 因為我查到的 library 不是有點龐大就是引入的相依套件有點多，這邊我時做的部分比較少，只需要 
[Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/ "Newtonsoft.Json") , [RestSharp](https://www.nuget.org/packages/RestSharp/106.11.8-alpha.0.7)  


How To Start?
----

### Upload Image To ImgUr 

記得 保留住 id , deletehash , link 其他還好

```C#

            EZImgUr.UploadImageResult result = EZImgUr.Helper.UploadImage(CLIENTID, AppDomain.CurrentDomain.BaseDirectory + "linmin.jpg");

            Response.Write("ID:" + result.data.id + " , " + "DELETE_HASH:" + result.data.deletehash + " , " + "URL:" + result.data.link);
   
            //Upload Result Binary :
            /*
               {
                  "data": {
                    "id": "icF4V7P",
                    "title": null,
                    "description": null,
                    "datetime": 1605683608,
                    "type": "image/jpeg",
                    "animated": false,
                    "width": 1600,
                    "height": 900,
                    "size": 304347,
                    "views": 0,
                    "bandwidth": 0,
                    "vote": null,
                    "favorite": false,
                    "nsfw": null,
                    "section": null,
                    "account_url": null,
                    "account_id": 0,
                    "is_ad": false,
                    "in_most_viral": false,
                    "tags": [],
                    "ad_type": 0,
                    "ad_url": "",
                    "in_gallery": false,
                    "deletehash": "efwpG4ANupJWMZ1",
                    "name": "",
                    "link": "https://i.imgur.com/icF4V7P.jpg"
                  },    
                  "success": true,
                  "status": 200
                } 

            */


```



### Create Album



```C#

           
            EZImgUr.CreateAlbumResult result = EZImgUr.Helper.CreateAlbum(CLIENTID, "測試相簿!!!", "!!相簿敘述!!!", "icF4V7P");

            Response.Write("ID:" + result.data.id + " , " + "DELETE_HASH:" + result.data.deletehash);
        
            //如果你得到了 album id 
            //則公開網址就是 https://imgur.com/a/[ALBUMID]
            //此範例  https://imgur.com/a/DytNW1A
            /*
             
             {
                       "data":{
                                      "id":"DytNW1A",
                                      "deletehash":"dKhBtaq3uUvD3nF"
                                      },
                       "success":true,
                       "status":200
               }
             
             */

           
           
     
```

### Add Image to Album

將一張圖片放入到建立的相簿，請注意 下面兩個參數 第二個參數是 image 的 deletehashes , 第三個數 相簿的 deletehash

```C#
            
            EZImgUr.AlbumBasicResult result = EZImgUr.Helper.AddImageToAlbum(CLIENTID, "JFxUUKYYvKmWdJt,efwpG4ANupJWMZ1".Split(','), "dKhBtaq3uUvD3nF");
            
            //{"data":true,"success":true,"status":200}
            
            
            
```

### Get All Images Info form Album

取得所有相簿的照片資訊

```C#
            EZImgUr.GetImageInfoFromAlbumResult result = EZImgUr.Helper.GetImageInfoFromAlbum(CLIENTID, "DytNW1A");

            /*
              {
                "data": [
                  {
                    "id": "h2pYC3S",
                    "title": null,
                    "description": null,
                    "datetime": 1605676983,
                    "type": "image/jpeg",
                    "animated": false,
                    "width": 1920,
                    "height": 2559,
                    "size": 702720,
                    "views": 3,
                    "bandwidth": 2108160,
                    "vote": null,
                    "favorite": false,
                    "nsfw": null,
                    "section": null,
                    "account_url": null,
                    "account_id": null,
                    "is_ad": false,
                    "in_most_viral": false,
                    "has_sound": false,
                    "tags": [],
                    "ad_type": 0,
                    "ad_url": "",
                    "edited": "0",
                    "in_gallery": false,
                    "link": "https://i.imgur.com/h2pYC3S.jpg"
                  },
                  {
                    "id": "icF4V7P",
                    "title": null,
                    "description": null,
                    "datetime": 1605683608,
                    "type": "image/jpeg",
                    "animated": false,
                    "width": 1600,
                    "height": 900,
                    "size": 304347,
                    "views": 1,
                    "bandwidth": 304347,
                    "vote": null,
                    "favorite": false,
                    "nsfw": null,
                    "section": null,
                    "account_url": null,
                    "account_id": null,
                    "is_ad": false,
                    "in_most_viral": false,
                    "has_sound": false,
                    "tags": [],
                    "ad_type": 0,
                    "ad_url": "",
                    "edited": "0",
                    "in_gallery": false,
                    "link": "https://i.imgur.com/icF4V7P.jpg"
                  }
                ],
                "success": true,
                "status": 200
              }

             */

```

### Get Album Info

取得相簿資訊

```C#
            EZImgUr.AlbumInfoResult result = EZImgUr.Helper.GetAlbumInfo(CLIENTID, "DytNW1A");
            /*
              {
              "data": {
                "id": "DytNW1A",
                "title": "測試相簿!!!",
                "description": "!!相簿敘述!!!",
                "datetime": 1605763053,
                "cover": "h2pYC3S",
                "cover_edited": null,
                "cover_width": 1920,
                "cover_height": 2559,
                "account_url": null,
                "account_id": null,
                "privacy": "hidden",
                "layout": "blog",
                "views": 1,
                "link": "https://imgur.com/a/DytNW1A",
                "favorite": false,
                "nsfw": false,
                "section": null,
                "images_count": 2,
                "in_gallery": false,
                "is_ad": false,
                "include_album_ads": false,
                "is_album": true,
                "images": [
                  {
                    "id": "h2pYC3S",
                    "title": null,
                    "description": null,
                    "datetime": 1605676983,
                    "type": "image/jpeg",
                    "animated": false,
                    "width": 1920,
                    "height": 2559,
                    "size": 702720,
                    "views": 3,
                    "bandwidth": 2108160,
                    "vote": null,
                    "favorite": false,
                    "nsfw": null,
                    "section": null,
                    "account_url": null,
                    "account_id": null,
                    "is_ad": false,
                    "in_most_viral": false,
                    "has_sound": false,
                    "tags": [],
                    "ad_type": 0,
                    "ad_url": "",
                    "edited": "0",
                    "in_gallery": false,
                    "link": "https://i.imgur.com/h2pYC3S.jpg"
                  },
                  {
                    "id": "icF4V7P",
                    "title": null,
                    "description": null,
                    "datetime": 1605683608,
                    "type": "image/jpeg",
                    "animated": false,
                    "width": 1600,
                    "height": 900,
                    "size": 304347,
                    "views": 1,
                    "bandwidth": 304347,
                    "vote": null,
                    "favorite": false,
                    "nsfw": null,
                    "section": null,
                    "account_url": null,
                    "account_id": null,
                    "is_ad": false,
                    "in_most_viral": false,
                    "has_sound": false,
                    "tags": [],
                    "ad_type": 0,
                    "ad_url": "",
                    "edited": "0",
                    "in_gallery": false,
                    "link": "https://i.imgur.com/icF4V7P.jpg"
                  }
                ],
                "ad_config": {
                  "safeFlags": [
                    "album",
                    "page_load",
                    "not_in_gallery"
                  ],
                  "highRiskFlags": [],
                  "unsafeFlags": [
                    "sixth_mod_unsafe"
                  ],
                  "wallUnsafeFlags": [],
                  "showsAds": false
                }
              },
              "success": true,
              "status": 200
            }
            
             
             */
```

### Delete Image

刪除圖片，下面放的參數是 image deletehash

```C#
            EZImgUr.AlbumBasicResult result = EZImgUr.Helper.DeleteImage(CLIENTID, "JFxUUKYYvKmWdJt");
            //{"data":true,"success":true,"status":200}

```


