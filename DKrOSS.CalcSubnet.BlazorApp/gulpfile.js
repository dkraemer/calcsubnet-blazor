// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

const gulp = require('gulp');
const exec = require('child_process').exec;
const moment = require('moment');
const fs = require('fs');
const del = require('del');

const tmpPublishDir = 'bin/publish';
const tmpPublishWwwDir = tmpPublishDir + '/wwwroot';
const tmpPublishFiles = tmpPublishWwwDir + '/**/*';
const publishDir = '../../calcsubnet-site';
const noJekyllFile = publishDir + '/.nojekyll';
const cNameFile = publishDir + '/CNAME';
const cNameFileContent = 'calcsub.net';
const gitAttributesFile = publishDir + '/.gitattributes';
const gitAttributesFileContent = '* binary';

const vendorCss = [
    'node_modules/bootstrap/dist/css/bootstrap.css',
    'node_modules/@fortawesome/fontawesome-free/css/all.css'
];

const vendorImg = [
    //'node_modules/@fortawesome/fontawesome-free/svgs/solid/calculator.svg'
];

const vendorJs = [
    'node_modules/bootstrap/dist/js/bootstrap.js',
    'node_modules/jquery/dist/jquery.slim.js',
    'node_modules/popper.js/dist/umd/popper.js'
];

const vendorWebfonts = [
    'node_modules/@fortawesome/fontawesome-free/webfonts/*.woff*'
];

const handleOutput = function(error, stdout, stderr) {
    if (error) {
        console.error(error.message);
        return;
    }
    if (stderr) {
        console.error(stderr);
        return;
    }
    console.log(stdout);
};

const dotnet = function(command) {
    return exec(`dotnet ${command}`, handleOutput);
};

gulp.task('copy-vendor-css',
    () => {
        return gulp.src(vendorCss)
            .pipe(gulp.dest('wwwroot/lib/css'));
    });

gulp.task('copy-vendor-img',
    () => {
        return gulp.src(vendorImg)
            .pipe(gulp.dest('wwwroot/lib/img'));
    });

gulp.task('copy-vendor-js',
    () => {
        return gulp.src(vendorJs)
            .pipe(gulp.dest('wwwroot/lib/js'));
    });

gulp.task('copy-vendor-webfonts',
    () => {
        return gulp.src(vendorWebfonts)
            .pipe(gulp.dest('wwwroot/lib/webfonts'));
    });

gulp.task('dotnet-publish', () => dotnet(`publish -c Release -o ${tmpPublishDir}`));

gulp.task('copy-published',
    () => {
        return gulp.src(tmpPublishFiles)
            .pipe(gulp.dest(publishDir));
    });


var timestamp = moment().format();
gulp.task('create-publish-files',
    function(cb) {
        fs.writeFile(noJekyllFile, timestamp, cb);
        fs.writeFile(cNameFile, cNameFileContent, cb);
        fs.writeFile(gitAttributesFile, gitAttributesFileContent, cb);
    });

gulp.task('copy-vendor', gulp.parallel('copy-vendor-css', 'copy-vendor-js', 'copy-vendor-webfonts'));
gulp.task('publish', gulp.series('dotnet-publish', 'copy-published', 'create-publish-files'));
gulp.task('default', gulp.series('copy-vendor'));