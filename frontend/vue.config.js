module.exports = {
  assetsDir: '',
  outputDir: '../frontend/dist',
  publicPath: '/frontend/dist/',
  filenameHashing: false,
  transpileDependencies: ['vuetify'],
  css: {
    extract: {
      filename: 'css/[name].css',
      chunkFilename: 'css/[name].css',
    },
    loaderOptions: {
      sass: {
        additionalData: `@import "@/styles/variables.scss";`
      }
    }
  }
};