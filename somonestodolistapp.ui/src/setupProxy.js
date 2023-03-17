const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/todo",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'http://localhost:62116',
        secure: false
    });

    app.use(appProxy);
};
