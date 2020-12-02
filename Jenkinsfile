pipeline {
  agent any
  stages {
    // stage('Verify Branch') {
    //   steps {
    //     echo "$GIT_BRANCH"
    //   }
    // }
    stage('Pull Changes') {
      steps {
        powershell(script: "git pull origin main")
      }
    }
	stage('Run Unit Tests') {
      steps {
        powershell(script: """ 
          cd Server
		  cd Seller.Server
          dotnet test
          cd ..
		  cd ..
        """)
      }
    }
    stage('Docker Build') {
      steps {
        powershell(script: 'docker-compose build')
		powershell(script: 'docker build -t plvasilev/seller-user-client-production --build-arg configuration=production ./Client/Seller.Client')		
        powershell(script: 'docker images -a')
      }
	  post {
	    success {
	      echo "Docker Build successfull!"
	    }
	    failure {
	      powershell(script: 'docker-compose down')
	    }
      }
    }
    stage('Run Test Application') {
      steps {
        powershell(script: 'docker-compose up -d')	
      }
	  post {
	    success {
	      echo "Run Test Application successfull!"
	    }
	    failure {
	      powershell(script: 'docker-compose down')
	    }
      }
    }
	stage('Run Integration Tests') {
      steps {
        powershell(script: './Tests/ContainerTestsProd.ps1') 
      }
	  post {
	    success {
	      echo "Run Integration successfull!"
	    }
	    failure {
	      powershell(script: 'docker-compose down')
	    }
      }
    }
    stage('Stop Test Application') {
      steps {
        powershell(script: 'docker-compose down')  		
      }
    }
	stage('Push Images') {
      steps {
        script {
          docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
            def image = docker.image("plvasilev/seller-identity-service")
            image.push("1.0.${env.BUILD_ID}")
            image.push('latest')
			}
          docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
            def image = docker.image("plvasilev/seller-listings-service")
            image.push("1.0.${env.BUILD_ID}")
            image.push('latest')
			}
          docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
            def image = docker.image("plvasilev/seller-offers-service")
            image.push("1.0.${env.BUILD_ID}")
            image.push('latest')
			}
          docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
            def image = docker.image("plvasilev/seller-contactus-service")
            image.push("1.0.${env.BUILD_ID}")
            image.push('latest')
			}
          docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
            def image = docker.image("plvasilev/seller-notifications-service")
            image.push("1.0.${env.BUILD_ID}")
            image.push('latest')
			}
          docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
            def image = docker.image("plvasilev/seller-gateway-service")
            image.push("1.0.${env.BUILD_ID}")
            image.push('latest')
			}
          docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
            def image = docker.image("plvasilev/seller-administration-service")
            image.push("1.0.${env.BUILD_ID}")
            image.push('latest')
			}
          docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
            def image = docker.image("plvasilev/seller-watchdog-service")
            image.push("1.0.${env.BUILD_ID}")
            image.push('latest')
			}
          docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
            def image = docker.image("plvasilev/seller-client-service")
            image.push("1.0.${env.BUILD_ID}")
            image.push('latest')			
          }
        }
      }
    }
	stage('Deploy Production') {
      steps {
        withKubeConfig([credentialsId: 'ProductionServer', serverUrl: 'https://34.72.91.63']) {
		       powershell(script: 'kubectl apply -f ./.k8s/.environment/production.yml') 
		       powershell(script: 'kubectl apply -f ./.k8s/databases') 
		       powershell(script: 'kubectl apply -f ./.k8s/event-bus') 
		       powershell(script: 'kubectl apply -f ./.k8s/web-services') 
			   powershell(script: 'kubectl apply -f ./.k8s/clients') 
               powershell(script: 'kubectl set image deployments/user-client user-client=plvasilev/seller-user-client-production:1.0.${env.BUILD_ID}')
        }
      }
      post {
	    success {
	      echo "Build Production successfull! :)"
		  mail to: 'pvvasilev2013@gmail.com',
               // cc: 'ccedpeople@gamil.com'
               subject: "SUCCESSFUL:Production Build ${env.JOB_NAME}", 
               body: "Build Successful ${env.JOB_NAME} build no: ${env.BUILD_NUMBER}\n\nView the log at:\n ${env.BUILD_URL}\n\nBlue Ocean:\n${env.RUN_DISPLAY_URL}"
        }
	    failure {
	      echo "Build Production failed! You should look into it!"
		  mail to: 'pvvasilev2013@gmail.com',
               // cc: 'ccedpeople@gamil.com'
               subject: "FAILED: Production Build ${env.JOB_NAME}", 
               body: "Build failed ${env.JOB_NAME} build no: ${env.BUILD_NUMBER}.\n\nView the log at:\n ${env.BUILD_URL}\n\nBlue Ocean:\n${env.RUN_DISPLAY_URL}"
	    }
      }
    }
  }
}