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
  }
}