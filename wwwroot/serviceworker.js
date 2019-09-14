var cacheName = 'io-celin-blasve';
var filesToCache = [
    '/_framework/blazor.server.js',
    // Our additional files
    '/manifest.json',
    '/serviceworker.js',
    // Icons
    '/favicon.ico',
    '/icon-192x192.png',
    '/icon-512x512.png',
    // JS
    '/main.js',
    '/main.css',
    // Fonts
    '/663974c9fe3ba55b228724fd4d4e445f.ttf',
    '/3156116d1667eea051f96b697f069624.ttf',
    // Pages
    '/index'
];

self.addEventListener('install', function (e) {
    console.log('[ServiceWorker] Install');
    e.waitUntil(
        caches.open(cacheName).then(function (cache) {
            console.log('[ServiceWorker] Caching app shell');
            return cache.addAll(filesToCache);
        })
    );
});

self.addEventListener('activate', event => {
    event.waitUntil(self.clients.claim());
});

self.addEventListener('fetch', event => {
    event.respondWith(
        caches.match(event.request, { ignoreSearch: true }).then(response => {
            return response || fetch(event.request);
        })
    );
});
