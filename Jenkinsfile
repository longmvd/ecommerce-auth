/* groovylint-disable Indentation */
/* groovylint-disable-next-line NglParseError */
pipeline {
    agent any
    stages {
        stage('Clone code') {
            steps {
                script {
                    git branch: 'main', credentialsId: 'github-longmvd', url: 'https://github.com/longmvd/ecommerce-auth.git'
                    def directoryToDelete = 'Lib'

                    // Check if the directory exists before attempting to delete
                    if (fileExists(directoryToDelete)) {
                        // Remove the directory
                        deleteDir()
                    }
                    sh 'cp -R ../Lib .'
                }
            }
        }

        stage('Build and Push Docker Image') {
            steps {
                withDockerRegistry(credentialsId: 'docker-hub', url: 'https://index.docker.io/v1/') {
                    echo 'Building Docker image...'
                    sh 'docker build -t giadienanhkysi/ecommerce-auth .'
                    echo 'Pushing Docker image to DockerHub...'
                    sh 'docker push giadienanhkysi/ecommerce-auth'
                }
            }
        }

        stage('Deploy to remote server') {
            steps {
                script {
                    /* groovylint-disable-next-line NestedBlockDepth, SpaceAfterClosingBrace */
                    def remoteServer = '14.225.204.198'
                    def remoteUser = 'root'
                    def remoteDir = '/home/ecommerce-production'

                    // Start the SSH agent and add the credentials
                    sshagent(['ecommerce-server']) {
                        // SSH to the remote server and execute commands
                        sh """
                           ssh -o StrictHostKeyChecking=no -l ${remoteUser} ${remoteServer} "cd ${remoteDir} && docker pull giadienanhkysi/ecommerce-auth && pwd && docker compose up -d"
                        """
                    }
                }
            }
        }
    }
}
