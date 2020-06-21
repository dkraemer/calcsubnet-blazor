const gulp = require('gulp');

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
    'node_modules/@fortawesome/fontawesome-free/webfonts/*'
];

gulp.task('copy-vendor-css', () => {
    return gulp.src(vendorCss)
        .pipe(gulp.dest('wwwroot/lib/css'));
});

gulp.task('copy-vendor-img', () => {
    return gulp.src(vendorImg)
        .pipe(gulp.dest('wwwroot/lib/img'));
});

gulp.task('copy-vendor-js', () => {
    return gulp.src(vendorJs)
        .pipe(gulp.dest('wwwroot/lib/js'));
});

gulp.task('copy-vendor-webfonts', () => {
    return gulp.src(vendorWebfonts)
        .pipe(gulp.dest('wwwroot/lib/webfonts'));
});

gulp.task('copy-vendor', gulp.parallel('copy-vendor-css', 'copy-vendor-js', 'copy-vendor-webfonts'));
gulp.task('default', gulp.series('copy-vendor'));
