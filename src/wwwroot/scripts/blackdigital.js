window.blackdigital = {
    scripts: {
        contains: (url) => {
            if (!url)
                return false

            var scripts = document.getElementsByTagName('script')

            for (var i = scripts.length; i--;) {
                if (scripts[i].src == url) return true;
            }

            return false;
        },
        load: (sourceUrl) => {
            return new Promise(function (myResolve, myReject) {
                console.log(`Script ${sourceUrl} to load`)

                if (sourceUrl.Length == 0) {
                    console.error("Invalid source URL")
                    myReject("Invalid source URL")
                }

                if (!window.blackdigital.scripts.contains(sourceUrl)) {
                    var tag = document.createElement('script')
                    tag.src = sourceUrl
                    tag.type = "text/javascript"

                    tag.onload = function () {
                        console.log(`Script ${sourceUrl} loaded successfully`)
                        myResolve(true)
                    }

                    tag.onerror = function () {
                        console.error(`Failed to load script ${sourceUrl}`)
                        myResolve(false)
                    }

                    document.body.appendChild(tag)
                }
                else
                    myResolve(true)
            })
        },
    },
    elements: {
        scrollIntoView: (element) => element?.scrollIntoView(),
        fullscreen: (element, eventElement) => {
            if (document.fullscreenElement) {
                document.exitFullscreen();
            }
            else {
                element.requestFullscreen();
            }

            element.addEventListener('fullscreenchange', (event) => {
                event.stopImmediatePropagation();

                if (eventElement && !document.fullscreenElement) {
                    eventElement.invokeMethodAsync('OnCloseFullScreen');
                }
            })
        }
    },
    dbs: {
        dbList: {},
        create: async (builder) => {
            return new Promise((resolve, reject) => {
                if (!window.indexedDB) {
                    console.log("Your browser doesn't support a stable version of IndexedDB. Such and such feature will not be available.")
                    reject()
                }

                var dbRequest = window.indexedDB.open(builder.name, builder.version)

                dbRequest.onerror = function (event) {
                    console.log(`Error opening database ${builder.name}`)
                    reject()
                }

                dbRequest.onsuccess = function (event) {
                    console.log(`Database ${builder.name} opened successfully`)
                    window.blackdigital.dbs.dbList[builder.name] = dbRequest.result
                    resolve()
                }

                dbRequest.onupgradeneeded = function (event) {
                    console.log(`Database ${builder.name} upgrade needed`)

                    var db = event.target.result

                    for (var i = 0; i < builder.stores.length; i++) {
                        var store = builder.stores[i]

                        if (!db.objectStoreNames.contains(store.name)) {
                            var objectStore = db.createObjectStore(store.name, { keyPath: store.keyPath })

                            for (var j = 0; j < store.indexes.length; j++) {
                                var index = store.indexes[j]
                                objectStore.createIndex(index.name, index.keyPath, { unique: index.unique })
                            }
                        }
                    }
                }
            });
        },
        containsDb: (dbName) => {
            if (window.blackdigital.dbs.dbList[dbName])
                return true

            return false
        },
        getDb: (dbName, storeName, mode) => {
            if (window.blackdigital.dbs.dbList[dbName]) {
                let db = window.blackdigital.dbs.dbList[dbName]
                let tx = db.transaction(storeName, mode)
                return tx.objectStore(storeName)
            }

            return null
        },
        getAll: async (dbName, storeName) => {
            return new Promise(async (resolve, reject) => {
                const objectStore = await window.blackdigital.dbs.getDb(dbName, storeName, "readonly")

                const request = objectStore.getAll()
                request.onsuccess = function (event) {
                    resolve(event.target.result)
                }
                request.onerror = function (event) {
                    console.error("get:", event.target.errorCode)
                    reject()
                }
            })
        },
        get: async (dbName, storeName, key) => {
            return new Promise(async (resolve, reject) => {
                const objectStore = await window.blackdigital.dbs.getDb(dbName, storeName, "readonly")

                const request = objectStore.get(key)
                request.onsuccess = function (event) {
                    resolve(event.target.result)
                }
                request.onerror = function (event) {
                    console.error("get:", event.target.errorCode)
                    reject()
                }
            })
        },
        insert: async (dbName, storeName, value) => {
            return new Promise(async (resolve, reject) => {
                const objectStore = await window.blackdigital.dbs.getDb(dbName, storeName, "readwrite")

                const request = objectStore.add(value)
                request.onsuccess = function (event) {
                    resolve()
                }
                request.onerror = function (event) {
                    console.error("add:", event.target.errorCode)
                    reject()
                }
            })
        },
        delete: async (dbName, storeName, key) => {
            return new Promise(async (resolve, reject) => {
                const objectStore = await window.blackdigital.dbs.getDb(dbName, storeName, "readwrite")

                const request = objectStore.delete(key)
                request.onsuccess = function (event) {
                    resolve()
                }
                request.onerror = function (event) {
                    console.error("add:", event.target.errorCode)
                    reject()
                }
            })
        },
        save: async (dbName, storeName, key, value) => {
            await window.blackdigital.dbs.delete(dbName, storeName, key)
            await window.blackdigital.dbs.insert(dbName, storeName, value)
        },
        clear: async (dbName, storeName) => {
            const objectStore = await window.blackdigital.dbs.getDb(dbName, storeName, "readwrite")
            objectStore.clear()
        }
    }
}