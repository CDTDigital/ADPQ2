var gulp = require('gulp');
var execSync = require('sync-exec');
var Builder = require('systemjs-builder');
var exec = require('child_process').exec;

var webroot = './wwwroot';


gulp.task("ScriptsNStyles", function () {
    gulp.src([
            'systemjs/dist/system-polyfills.js',
            'systemjs/dist/system.src.js',
            //'reflect-metadata/Reflect.js',
            'rxjs/**/*.{js,js.map}',
            'zone.js/dist/**/*.js',
            '@angular/**/*.{js,js.map}',
            'bootstrap/dist/**',
            'font-awesome/css/font-awesome.css',
            'font-awesome/fonts/*.*',
            'primeng/**/*.{js,js.map}',
            'primeng/resources/primeng.css',
            'primeng/resources/themes/bootstrap/theme.css',
            'angular2-cookie/**/*.{js,js.map}',
            "core-js/**/*.js", "!core-js/**/*.min.js"

    ], {
        cwd: "node_modules/**"
    })
        .pipe(gulp.dest(webroot + '/lib/'));

    gulp.src(['plugins/*.js'], { cwd: "App/**" }).pipe(gulp.dest(webroot + '/lib/'));

    execSync("gulp ng-bundle");
    return;
});

gulp.task('ng-bundle', function (done) {

    var builder = new Builder("./node_modules", `${webroot}/systemjs.config.js`);
    //
    // RXJS
    //
    builder
        .bundle([
            `./node_modules/rxjs/add/**/*.js`,
            `./node_modules/rxjs/observable/**/*.js`,
            `./node_modules/rxjs/operator/**/*.js`,
            `./node_modules/rxjs/scheduler/**/*.js`,
            `./node_modules/rxjs/symbol/**/*.js`,
            `./node_modules/rxjs/util/**/*.js`,
            `./node_modules/rxjs/*.js`
        ], `${webroot}/lib/bundles/rxjs.min.js`, {
            minify: true,
            sourceMaps: true,
            mangle: false
        })
        .then(function () {
            console.log('Build complete');
        })
        .catch(function (err) {
            console.log('Build error');
            console.log(err);
        });


    //
    // PRIMENG
    //
    builder
        .bundle([
            `./node_modules/primeng/components/**/*.js`
        ], `${webroot}/lib/bundles/primeng.min.js`, {
            minify: true,
            sourceMaps: true,
            mangle: false
        })
        .then(function () {
            console.log('Build complete');
            done();
        })
        .catch(function (err) {
            console.log('Build error');
            console.log(err);
            done();
        });
});

gulp.task('installTypings', function () {
    console.log("now installing the typings utility");
    exec('npm install typings --global', function (err, stdout, stderr) {
        console.log(stdout);
        if (err) {
            console.log('(1) install typings --global exited with error code', err.message);
            return;
        }

        console.log("now installing the typings defined in typings.json");
        exec('typings install --global', function (err, stdout, stderr) {
            console.log(stdout);
            if (err) {
                console.log('(2) typings --global exited with error code', err.message);
                return;
            }
        });
    });
});