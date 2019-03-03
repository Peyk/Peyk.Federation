const $ = require('shelljs')
const path = require('path')
require('../logging')

$.config.fatal = true
const root = path.resolve(`${__dirname}/../..`)
const deploy_settings = require('./../deploy/deploy_settings')
const docker_deployment = require('./../deploy/deploy_docker_registry')

try {
    console.info(`BUILDING DOCKER IMAGES`)

    $.cd(root)

    console.debug('building the final web app with "peyk-client-server:latest" tag')
    $.exec(`
        docker                          \
        build                           \
        --tag peyk-client-server        \
        --target final-client-server    \
        .
    `)

    console.debug('building the solution with "Release" configuration and "peyk-federation:solution" tag')
    $.exec(`
        docker                              \
        build                               \
        --tag peyk-federation:solution      \
        --no-cache                          \
        --target solution-build             \
        --build-arg "configuration=Release" \
        .
    `)

    console.debug('building the event store image with "peyk-eventstore:latest" tag')
    $.exec(`
        docker                  \
        build                   \
        --tag peyk-eventstore   \
        --target eventstore     \
        .
    `)

    console.debug('reading Docker deployment options')
    const docker_options = deploy_settings.get_docker_settings()
    if (docker_options) {
        console.debug('pushing images to the Docker hub')

        docker_deployment.deploy(
            'peyk-federation:solution',
            'peyk/federation:unstable-solution',
            docker_options.user,
            docker_options.pass
        )

        docker_deployment.deploy(
            'peyk-client-server:latest',
            'peyk/client-server:unstable',
            docker_options.user,
            docker_options.pass
        )

        docker_deployment.deploy(
            'peyk-eventstore',
            'peyk/eventstore',
            docker_options.user,
            docker_options.pass
        )
    } else {
        console.warn('Docker deployment options not found. skipping Docker image push...')
    }
} catch (e) {
    console.error(`❎ AN UNEXPECTED ERROR OCURRED`)
    console.error(e)
    process.exit(1)
}

console.info(`✅ BUILD SUCCEEDED`)
