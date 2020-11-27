pipeline {
  agent any
  stages {
    stage('Verify Branch') {
      steps {
        echo "$GIT_BRANCH"
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
        powershell(script: 'docker images -a')
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
        powershell(script: './Tests/ContainerTests.ps1') 
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
      post {
	    success {
	      echo "Build successfull! You should deploy! :)"
	    }
	    failure {
	      echo "Build failed! You should receive an e-mail! :("
	    }
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
  }
}