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
        openDb: async () => {
        },
        containsDb: (dbName) => {
            if (window.blackdigital.dbs.dbList[dbName])
                return true

            return false
        },
        getDb: (dbName) => {
            if (window.blackdigital.dbs.dbList[dbName])
                return window.blackdigital.dbs.dbList[dbName]

            return null
        }
    }
}