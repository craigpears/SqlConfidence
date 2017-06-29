module.exports = function(grunt) {
	var cssFiles = ["../app/styles/*.scss", "../app/styles/**/*.scss"];
	grunt.initConfig({
		sass: {
			all: {
				files: {
					'../app_built/styles.css' : '../app/styles/base.scss'
				}
			}
		},
		cssmin: {
			target: {
				files: {
				  '../app_built/styles.min.css': ['../app_built/styles.css']
				}
			}
		},
		watch: {
		  files: cssFiles,
		  tasks: ['styles']
		}
	});
	
	grunt.loadNpmTasks('grunt-contrib-watch');
	grunt.loadNpmTasks('grunt-contrib-sass');
	grunt.loadNpmTasks('grunt-contrib-cssmin');

	grunt.registerTask('styles', ['sass','cssmin']);
	grunt.registerTask('default', ['styles', 'watch']);
	grunt.registerTask('dist', ['styles']);
};