const path = require('path')
const webpack = require('webpack')
const ExtractTextPlugin = require('extract-text-webpack-plugin')
const dest = './wwwroot/dist'

module.exports = (env) => {
  const prod = process.env.NODE_ENV == 'production'

  return {
    stats: { modules: false },
    entry: {
      'app': './ClientApp/boot.js'
    },
    resolve: {
      extensions: ['.js', '.vue'],
      alias: {
        'vue$': 'vue/dist/vue.esm.js'
      }
    },
    output: {
      path: path.join(__dirname, dest),
      filename: '[name].js',
      publicPath: '/dist/'
    },
    module: {
      rules: [
        { test: /\.vue$/, include: /ClientApp/, use: 'vue-loader' },
        { test: /\.js$/, include: /ClientApp/, use: 'babel-loader' },
        { test: /\.css$/, use: !prod ? ['style-loader', 'css-loader'] : ExtractTextPlugin.extract({ use: 'css-loader?minimize' }) }
      ]
    },
    plugins: [
      new webpack.optimize.CommonsChunkPlugin({
        name: 'vendor',
        chunks: ['app'],
        minChunks(module) {
          return module.context && (/node_modules/).test(module.context)
        }
      }),
      new webpack.optimize.CommonsChunkPlugin({
        name: 'manifest'
      }),
      new webpack.SourceMapDevToolPlugin({
        filename: '[file].map',
        moduleFilenameTemplate: path.relative(dest, '[resourcePath]')
      })
    ].concat(prod ? [
      new ExtractTextPlugin("site.css")
    ] : [])
  }
}