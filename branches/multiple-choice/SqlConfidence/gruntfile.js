module.exports = function(grunt) {
	var cssFiles = ["app/styles/*.css", "app/styles/**/*.css"];
	grunt.initConfig({
	  concat_css: {
		options: {
		  // Task-specific options go here. 
		},
		all: {
		  src: cssFiles,
		  dest: "app_built/styles.css"
		},
	  },
	  watch: {
		  files: cssFiles,
		  tasks: ['concat_css']
	  }
	});
	
	grunt.loadNpmTasks('grunt-concat-css');
	grunt.loadNpmTasks('grunt-contrib-watch');

	grunt.registerTask('default', ['concat_css', 'watch']);
};