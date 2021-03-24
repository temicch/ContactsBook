// webpack.config.js
const path = require("path");
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const TerserPlugin = require("terser-webpack-plugin");

let config = {
    entry: {
        main: './ClientApp/src/main.ts'
    },
    output: {
        path: path.resolve(__dirname, 'ClientApp/dist'),
        publicPath: '/styles',
        filename: '[name].js'
    },
    resolve: {
      alias: {
        // Ensure the right Vue build is used
        'vue$': 'vue/dist/vue.esm.js'
        }   
    },
    optimization: {
      minimize: true,
      minimizer: [new TerserPlugin()],
    },
    module: {
        rules: [
            {
                test: /\.ts$/,
                use: 'ts-loader',
                exclude: /node_modules/
            },
            {
                // Look for JavaScript files and process them according to the
                // rules specified in the different loaders
                test: /\.(js)$/,

                // Ignore the node_modules directory
                exclude: /node_modules/,

                // Use Babel to transpile ES6+ to ES5
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env']
                    }
                }
            },
            {
              test: /\.s[ac]ss$/i,
              use: [
                // Creates `style` nodes from JS strings
                "style-loader",
                // Translates CSS into CommonJS
                "css-loader",
                // Compiles Sass to CSS
                "sass-loader",
              ],
            },
            {
                test: /\.css$/i,
                use: ["style-loader", "css-loader"],
            },
            {
                test: /\.svg/,
                type: 'asset/inline'
            },
        ]
    },
    plugins: [
        new CleanWebpackPlugin(),
    ],
};

module.exports = (env, argv) => {
    if(argv.mode == 'development') {
        config.devtool = 'inline-source-map';
    } 
    //else {
    //     config.devtool = 'source-map';
    // }
    return config;
};