/// <binding BeforeBuild='css-clean, sass-to-css, clean-bundle, bundle-min' ProjectOpened='sass-to-css:watch' />
"use strict";
var gulp = require("gulp"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    sass = require("gulp-sass"),
    sourcemap = require("gulp-sourcemaps"),
    uglify = require("gulp-uglify"),
    merge = require("merge-stream"),
    del = require("del"),
    cleanCSS = require("gulp-clean-css"),
    clean = require("gulp-clean"),
    bundleconfig = require("./bundleconfig.json"); 
	
var regex = {
    css: /\.css$/,
    html: /\.(html|htm)$/,
    js: /\.js$/
};


/* Gulp to convert all Scss to css while saving */

gulp.task('sass-to-css', function () {
    return gulp.src(['./wwwroot/css/**/*.scss',
        , '!./wwwroot/css/Common/*.scss'])
        .pipe(sass.sync().on('error', sass.logError))
        .pipe(gulp.dest('./wwwroot/dist/css'));
});

gulp.task('sass-to-css:watch', function () {
    gulp.watch('./wwwroot/css/**/*.scss', ['sass-to-css']);
});

gulp.task('css-clean', function () {
    return gulp.src(['./wwwroot/dist/**/*.css',
        './wwwroot/dist/**/*.js',])
        .pipe(clean());
});

/*Minifying using Bundle*/

gulp.task("bundle-min", ["min:js", "min:css"]);

gulp.task("min:js", function () {
    var tasks = getBundles(regex.js).map(function (bundle) {
        return gulp.src(bundle.inputFiles, { base: "." })
            .pipe(concat(bundle.outputFileName))
            .pipe(uglify())
            .pipe(gulp.dest("."));
    });
    return merge(tasks);
});

gulp.task("min:css", function () {
    var tasks = getBundles(regex.css).map(function (bundle) {
        return gulp.src(bundle.inputFiles, { base: "." })
            .pipe(concat(bundle.outputFileName))
            .pipe(cssmin())
            .pipe(gulp.dest("."));
    });
    return merge(tasks);
});

gulp.task("clean-bundle", function () {
    var files = bundleconfig.map(function (bundle) {
        return bundle.outputFileName;
    });

    return del(files);
});

gulp.task("bundle-watch", function () {
    getBundles(regex.js).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:js"]);
    });

    getBundles(regex.css).forEach(function (bundle) {
        gulp.watch(bundle.inputFiles, ["min:css"]);
    });
});

function getBundles(regexPattern) {
    return bundleconfig.filter(function (bundle) {
        return regexPattern.test(bundle.outputFileName);
    });
}
