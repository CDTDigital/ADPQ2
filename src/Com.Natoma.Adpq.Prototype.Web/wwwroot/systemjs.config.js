﻿(function (global) {
    // map tells the System loader where to look for things
    var map = {
        'app': 'js', // 'dist',
        'rxjs': 'lib/rxjs',
        //'angular2-in-memory-web-api': 'lib/angular2-in-memory-web-api',
        '@angular': 'lib/@angular',
        'primeng': 'lib/primeng',
        //'angular2-cookie': 'lib/angular2-cookie',
    };
    // packages tells the System loader how to load when no filename and/or no extension
    var packages = {
        'app': { main: 'main.js', defaultExtension: 'js' },
        //'angular2-in-memory-web-api': { defaultExtension: 'js' },
        //'angular2-cookie': { defaultExtension: 'js' },
    };
    var ngPackageNames = [
      'common',
      'compiler',
      'core',
      'forms',
      'http',
      'platform-browser',
      'platform-browser-dynamic',
      'router',
      //'upgrade',
    ];
    // Individual files (~300 requests):
    function packIndex(pkgName) {
        packages['@angular/' + pkgName] = { main: 'index.js', defaultExtension: 'js' };
        
    }
    // Bundled (~40 requests):
    function packUmd(pkgName) {
        packages['@angular/' + pkgName] = { main: '/bundles/' + pkgName + '.umd.js', defaultExtension: 'js' };
    }
    // Most environments should use UMD; some (Karma) need the individual index files
    var setPackageConfig = System.packageWithIndex ? packIndex : packUmd;
    // Add package entries for angular packages
    ngPackageNames.forEach(setPackageConfig);

    var config = {
        map: map,
        packages: packages,
        bundles: {
            "lib/bundles/rxjs.min.js": ["rxjs/*"],
            "lib/bundles/primeng.min.js": ["primeng/*"]
        }
    };
    System.config(config);
})(this);