const path = require("path");
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
const TerserPlugin = require("terser-webpack-plugin");
const { VueLoaderPlugin } = require("vue-loader");
var webpack = require("webpack");

let config = {
  entry: {
    main: "./ClientApp/src/main.ts",
  },
  output: {
    path: path.resolve(__dirname, "ClientApp/dist"),
    publicPath: "/styles",
    filename: "[name].js",
  },
  resolve: {
    alias: {
      vue$: "vue/dist/vue.esm.js",
      "@": path.resolve(__dirname, "ClientApp/src"),
    },
    extensions: [".ts", ".js", ".json", ".vue"],
  },
  optimization: {
    minimize: true,
    minimizer: [new TerserPlugin()],
  },
  module: {
    rules: [
      {
        test: /\.vue$/,
        loader: "vue-loader",
        exclude: /node_modules/,
      },
      {
        test: /\.ts$/,
        loader: "ts-loader",
        options: { appendTsSuffixTo: [/\.vue$/] },
        exclude: /node_modules/,
      },
      {
        test: /\.(js)$/,
        exclude: /node_modules/,

        // Use Babel to transpile ES6+ to ES5
        use: {
          loader: "babel-loader",
          options: {
            presets: ["@babel/preset-env"],
          },
        },
      },
      {
        test: /\.scss$/,
        use: ["vue-style-loader", "css-loader", "sass-loader"],
      },

      {
        test: /\.css$/i,
        use: ["style-loader", "vue-style-loader", "css-loader"],
      },
      {
        test: /\.svg/,
        use: ["vue-loader", "vue-svg-loader"],
      },
    ],
  },
  plugins: [
    new VueLoaderPlugin(),
    new CleanWebpackPlugin(),
    new webpack.DefinePlugin({
      "process.env.BUILD": JSON.stringify("web"),
    }),
  ],
};

module.exports = (env, argv) => {
  if (argv.mode == "development") {
    config.devtool = "inline-source-map";
    config.optimization.minimize = false;
  }
  return config;
};
