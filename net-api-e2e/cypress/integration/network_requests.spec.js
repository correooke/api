/// <reference types="cypress" />

context('Network Requests', () => {
  beforeEach(() => {
    
  })

  it('Test Basic Path', () => {
      cy.request({
        url: 'http://apiInfo/IpInfo',
        qs: {
          ip: '1.2.3.4',
        },
      })
      .should((response) => {
        expect(response.status).to.eq(200)
        expect(response.body).to.have.property('ip').and.be.equal("1.2.3.4")
        expect(response.body).to.have.property('countryCode').and.be.equal("US")
        expect(response.body).to.have.property('distance').and.be.equal(8709)
        expect(response).to.have.property('headers')
      }).then(function () {
        cy.wait(100)
        cy.request({
          url: 'http://statsBalancer/IpStats',
        })
        .should((response) => {
          expect(response.status).to.eq(200)
          expect(response.body).to.have.property('maxDistance').and.be.equal(8709)
          expect(response.body).to.have.property('minDistance').and.be.equal(8709)
          expect(response.body).to.have.property('totalCalls').and.be.equal(1)
          expect(response).to.have.property('headers')
        }).then(function () {      

          cy.request({
            url: 'http://apiInfo/IpInfo',
            qs: {
              ip: '83.44.196.93',
            },
          })
          .should((response) => {
            expect(response.status).to.eq(200)
            expect(response.body).to.have.property('ip').and.be.equal("83.44.196.93")
            expect(response.body).to.have.property('countryCode').and.be.equal("ES")
            expect(response.body).to.have.property('distance').and.be.equal(10283)
            expect(response).to.have.property('headers')
          }).then(function () {      
          
            cy.request({
              url: 'http://apiInfo/IpInfo',
              qs: {
                ip: '83.44.196.93',
              },
            }).then(function () {     
              cy.wait(3000)
              cy.request({
                url: 'http://statsBalancer/IpStats',
              })
              .should((response) => {
                expect(response.status).to.eq(200)
                expect(response.body).to.have.property('maxDistance').and.be.equal(10283)
                expect(response.body).to.have.property('minDistance').and.be.equal(8709)
                expect(response.body).to.have.property('totalCalls').and.be.equal(3)
                expect(response).to.have.property('headers')
              }) 
            })
          })
        })
      })       
  })

})
