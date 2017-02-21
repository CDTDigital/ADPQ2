var gulp = require('gulp');
var execSync = require('sync-exec');
var Builder = require('systemjs-builder');

var webroot = './wwwroot';


gulp.task("ScriptsNStyles", function () {
    gulp.src([
            'systemjs/dist/system-polyfills.js',
            'systemjs/dist/system.src.js',
            //'reflect-metadata/Reflect.js',
            'rxjs/**/*.{js,js.map}',
            'zone.js/dist/**/*.js',
            '@angular/**/*.{js,js.map}',
            //'jquery/dist/jquery.*js',
            //'jquery-validation/dist/jquery.validate.js',
            //'select2/dist/**/select2.{js,css}',
            //'jquery-validation-unobtrusive/jquery.validate.unobtrusive.js',
            //'jquery.cookie/jquery.cookie.js',
            //'jquery.uniform/dist/**',
            'bootstrap/dist/**',
            //'font-awesome/css/font-awesome.css',
            //'font-awesome/fonts/*.*',
            //'primeng/**/*.{js,js.map}',
            //'primeng/resources/primeng.css',
            //'angular2-cookie/**/*.{js,js.map}',
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
});