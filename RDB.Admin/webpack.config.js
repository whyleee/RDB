const path = require('path')
const webpack = require('webpack')
const ExtractTextPlugin = require('extract-text-webpack-plugin')
const OptimizeCSSPlugin = require('optimize-css-assets-webpack-plugin')
const UglifyJSPlugin = require('uglifyjs-webpack-plugin')

const dest = './wwwroot/dist'

module.exports = (env) => {
  const isDevBuild = process.env.NODE_ENV != 'production' && !(env && env.production)
  process.env.NODE_ENV = isDevBuild ? 'development' : 'production'

  return {
    stats: { modules: false },
    entry: {
      app: './ClientApp/app.js'
    },
    resolve: {
      alias: {
        vue$: 'vue/dist/vue.esm.js'
      }
    },
    output: {
      path: path.join(__dirname, dest),
      filename: '[name].js',
      publicPath: '/dist/'
    },
    module: {
      rules: [
        /* eslint-disable object-property-newline */
        { test: /\.vue$/, include: /ClientApp/, loader: 'vue-loader', options: isDevBuild ? undefined : { loaders: {
          stylus: ExtractTextPlugin.extract({ use: ['css-loader', 'stylus-loader'], fallback: 'vue-style-loader' }) } } },
        { test: /\.js$/, include: /ClientApp/, use: 'babel-loader' },
        { test: /\.styl$/, use: isDevBuild ? ['style-loader', 'css-loader', 'stylus-loader']
          : ExtractTextPlugin.extract({ use: ['css-loader', 'stylus-loader'], fallback: 'style-loader' }) }
        /* eslint-enable */
      ]
    },
    watchOptions: {
      aggregateTimeout: 300,
      poll: 1000
    },
    plugins: [
      new webpack.DefinePlugin({
        'process.env': {
          NODE_ENV: JSON.stringify(isDevBuild ? 'development' : 'production')
        }
      }),
      new webpack.optimize.CommonsChunkPlugin({
        name: 'vendor',
        chunks: ['app'],
        minChunks(module) {
          return module.context && (/(node_modules|stylus)/).test(module.context)
        }
      }),
      new webpack.SourceMapDevToolPlugin({
        filename: '[file].map',
        moduleFilenameTemplate: path.relative(dest, '[resourcePath]'),
        exclude: ['vendor.js']
      })
    ].concat(isDevBuild ? [
      new webpack.NamedModulesPlugin()
    ] : [
      new webpack.HashedModuleIdsPlugin(),
      new UglifyJSPlugin({
        sourceMap: true
      }),
      new OptimizeCSSPlugin(),
      new ExtractTextPlugin('[name].css')
    ])
  }
}
